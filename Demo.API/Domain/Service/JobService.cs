﻿using Demo.API.Domain.Repository;
using Demo.API.Domain.Model;
using RauchTech.Common.Model;
using System;
using System.Collections.Generic;

namespace Demo.API.Domain.Service
{
    public class JobService
    {
        private readonly JobRepository _jobRepository;

        public JobService(JobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        #region Change Data

        public Job Insert(Job job)
        {
            try
            {
                if (job.ID == 0)
                {
                    job = _jobRepository.Insert(job);
                }
                else
                {
                    throw new Exception("ID diferente de 0, avalie a utilização do PUT");
                }
            }
            catch
            {
                throw;
            }

            return job;
        }

        public Job Update(Job job)
        {
            try
            {
                if (job.ID == 0)
                {
                    throw new Exception("ID diferente de 0, avalie a utilização do POST");
                }
                else
                {
                    job = _jobRepository.Update(job);
                }
            }
            catch
            {
                throw;
            }

            return job;
        }

        public void Delete(long id)
        {
            try
            {
                if (id == 0)
                {
                    throw new Exception("ID inválido");
                }
                else
                {
                    _jobRepository.Delete(id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Retrieve Repository

        public Job Get(long id)
        {
            Job job;

            try
            {
                job = _jobRepository.Get(id);
            }
            catch
            {
                throw;
            }

            return job;
        }

        public PageModel<Job> Get(string title = null, string description = null, long? candidateID = null, PageModel<Job> page = null)
        {
            try
            {
                page = _jobRepository.Get(title: title, description: description, candidateID: candidateID, page: page);
            }
            catch
            {
                throw;
            }

            return page;
        }

        #endregion
    }
}
