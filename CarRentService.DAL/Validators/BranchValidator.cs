﻿using CarRentService.Common.Attributes;
using CarRentService.DAL.Entities;
using FluentValidation;

namespace CarRentService.DAL.Validators;

public class BranchValidator : AbstractValidator<Branch>
{
    public BranchValidator()
    {
        
    }
}