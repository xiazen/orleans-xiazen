﻿using System;
using Microsoft.Extensions.Options;
using Orleans.Runtime;

namespace Orleans.Configuration
{
    internal class GrainCollectionOptionsValidator : IConfigurationValidator
    {
        private IOptions<GrainCollectionOptions> options;

        public GrainCollectionOptionsValidator(IOptions<GrainCollectionOptions> options)
        {
            this.options = options;
        }

        public void ValidateConfiguration()
        {
            if (this.options.Value.CollectionAge <= this.options.Value.CollectionQuantum)
            {
                throw new OrleansConfigurationException(
                    $"{nameof(GrainCollectionOptions.CollectionAge)} is set to {options.Value.CollectionAge}. " +
                    $"{nameof(GrainCollectionOptions.CollectionAge)} must be bigger than {nameof(GrainCollectionOptions.CollectionQuantum)}, " +
                    $"which is set to {this.options.Value.CollectionQuantum}");
            }
            foreach(var classSpecificCollectionAge in this.options.Value.ClassSpecificCollectionAge)
            {
                if(classSpecificCollectionAge.Value <= TimeSpan.Zero)
                {
                    throw new OrleansConfigurationException($"{classSpecificCollectionAge.Key} CollectionAgeLimit is set to {classSpecificCollectionAge.Value}. CollectionAgeLimit must be greater than zero.");
                }
            }
        }
    }
}
