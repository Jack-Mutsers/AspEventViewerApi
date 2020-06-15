using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;
using Pomelo.EntityFrameworkCore.MySql;
using Contracts.Repository;
using Contracts.Logic;
using Contracts.Logger;
using AutoMapper;
using Entities.DataTransferObjects;

namespace Logics
{
    public class ReviewLogic : IReviewLogic
    {
        private ILoggerManager _logger;
        private IReviewRepository _repository;
        private IMapper _mapper;

        public ReviewLogic(ILoggerManager logger, RepositoryContext repositoryContext, IMapper mapper)
        {
            _logger = logger;
            _repository = new ReviewRepository(repositoryContext);
            _mapper = mapper;
        }

        public bool Create(ReviewForCreationDto reviewForCreation)
        {
            try
            {
                Review DataEntity = _mapper.Map<Review>(reviewForCreation);

                _repository.Create(DataEntity);
                _repository.Save();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateReview action: {ex.Message}");
                throw new Exception();
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var reviewDto = GetById(id);
                if (reviewDto == null)
                {
                    _logger.LogError($"Review with id: {id}, hasn't been found in db.");
                    return false;
                }

                Review review = _mapper.Map<Review>(reviewDto);

                _repository.Delete(review);
                _repository.Save();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteReview action: {ex.Message}");
                throw new Exception();
            }
        }

        public IEnumerable<ReviewDto> GetAllOpenReviews()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReviewDto> GetByEventDate(int event_date_id)
        {
            throw new NotImplementedException();
        }

        public ReviewDto GetById(int review_id)
        {
            throw new NotImplementedException();
        }

        public bool Update(ReviewForUpdateDto reviewForUpdate)
        {
            try
            {
                var reviewDto = GetById(reviewForUpdate.id);

                if (reviewDto == null)
                {
                    return false;
                }

                Review DataEntity = _mapper.Map<Review>(reviewDto);

                _mapper.Map(reviewForUpdate, DataEntity);

                _repository.Update(DataEntity);
                _repository.Save();

                _logger.LogError($"Updated Review with id: {DataEntity.id}");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateReview action: {ex.Message}");
                throw new Exception();
            }
        }
    }
}
