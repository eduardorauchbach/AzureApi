﻿using ATS.API.Model;
using ATS.API.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rauchbach.Common.Logging;
using System;
using System.Collections.Generic;

namespace ATS.API.Controllers
{
    [EnableCors("Policy1")]
    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly CustomLog _logger;
        private readonly JobService _jobService;

        public JobsController(CustomLogFactory logger, JobService jobService)
        {
            _logger = logger.CreateLogger<CustomLogFactory>();
            _jobService = jobService;
        }


        [HttpGet]
        public IActionResult Get(long? candidateID = null)
        {
            List<Job> jobs;
            ObjectResult response;

            try
            {
                _logger.LogCustom(LogLevel.Information, message: CustomLog.Begin);

                jobs = _jobService.Get(candidateID: candidateID);

                response = Ok(jobs);

                _logger.LogCustom(LogLevel.Information, message: CustomLog.Finish);
            }
            catch (Exception ex)
            {
                _logger.LogCustom(LogLevel.Error, exception: ex);;
                response = StatusCode(500, ex.Message);
            }

            return response;
        }

        // GET: api/Job/5
        [HttpGet("{id}", Name = "GetJob")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(long id)
        {
            Job job;
            ObjectResult response;

            try
            {
                _logger.LogCustom(LogLevel.Information, message: CustomLog.Begin);

                job = _jobService.Get(id);

                if (job != null)
                {
                    response = Ok(job);
                }
                else
                {
                    response = NotFound(string.Empty);
                }

                _logger.LogCustom(LogLevel.Information, message: CustomLog.Finish);
            }
            catch (Exception ex)
            {
                _logger.LogCustom(LogLevel.Error, exception: ex);;
                response = StatusCode(500, ex.Message);
            }

            return response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] Job job)
        {
            ObjectResult response;

            try
            {
                _logger.LogCustom(LogLevel.Information, message: CustomLog.Begin);

                job = _jobService.Insert(job);

                response = Ok(job);

                _logger.LogCustom(LogLevel.Information, message: CustomLog.Finish);
            }
            catch (Exception ex)
            {
                _logger.LogCustom(LogLevel.Error, exception: ex);;
                response = StatusCode(500, ex.Message);
            }

            return response;
        }

        // PUT: api/Job/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Put(long id, [FromBody] Job job)
        {
            ObjectResult response;

            try
            {
                _logger.LogCustom(LogLevel.Information, message: CustomLog.Begin);

                job.ID = id;
                job = _jobService.Update(job);

                response = Ok(job);

                _logger.LogCustom(LogLevel.Information, message: CustomLog.Finish);
            }
            catch (Exception ex)
            {
                _logger.LogCustom(LogLevel.Error, exception: ex);;
                response = StatusCode(500, ex.Message);
            }

            return response;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(long id)
        {
            ObjectResult response;

            try
            {
                _logger.LogCustom(LogLevel.Information, message: CustomLog.Begin);

                _jobService.Delete(id);

                response = Ok(string.Empty);

                _logger.LogCustom(LogLevel.Information, message: CustomLog.Finish);
            }
            catch (Exception ex)
            {
                _logger.LogCustom(LogLevel.Error, exception: ex);
                response = StatusCode(500, ex.Message);
            }

            return response;
        }
    }
}
