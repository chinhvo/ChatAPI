﻿using AutoMapper;
using Chat.Domain.Interfaces;
using Chat.Domain.Interfaces.Repository_Interfaces;
using Chat.Domain.Interfaces.Service_Interfaces;
using Chat.Domain.Models;
using Chat.Domain.ViewModels.Interest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Services.Services
{
    public class InterestService : IInterestService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Interest> _interestRepository;

        public InterestService(IMapper mapper, IRepository<Interest> interestRepository)
        {
            _mapper = mapper;
            _interestRepository = interestRepository;
        }

        public async Task<IEnumerable<InterestViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<InterestViewModel>>(
                await _interestRepository.GetAll());
        }

        public async Task<InterestViewModel> GetById(Guid id)
        {
            return _mapper.Map<InterestViewModel>(
                await _interestRepository.GetById(id));
        }

        public async Task<Interest> Add(InterestCreateViewModel newInterest)
        {
            return await _interestRepository.Add(
                _mapper.Map<Interest>(newInterest));
        }

        public async Task<Interest> Update(InterestUpdateViewModel updateInterest)
        {
            Interest existingInterest = await _interestRepository.GetById(updateInterest.Id);
            if (existingInterest == null)
            {
                return null;
            }

            return await _interestRepository.Update(
                _mapper.Map<Interest>(updateInterest));
        }

        public async Task<Interest> Remove(Guid id)
        {
            Interest interestToBeRemoved = await _interestRepository.GetById(id);
            if (interestToBeRemoved == null)
            {
                return null;
            }

            await _interestRepository.Remove(interestToBeRemoved);
            return interestToBeRemoved;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
