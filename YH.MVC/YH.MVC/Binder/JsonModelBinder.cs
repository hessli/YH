﻿using System;

            if (!request.ContentType.StartsWith("application/json", StringComparison.InvariantCultureIgnoreCase))
            {
                //根据Content type来判断，只有application/json这种content type的才会使用该ModelBinder，否则使用默认的Binder
                base.BindProperty(controllerContext, bindingContext, propertyDescriptor);
                return;
            }

            // need to skip properties that aren't part of the request, else we might hit a StackOverflowException
            string name = propertyDescriptor.Name;

            object objA = propertyBinder.BindModel(controllerContext, bindingContext);
            if (bindingContext.ModelMetadata.ConvertEmptyStringToNull && object.Equals(objA, string.Empty))
            {
                var q = "";
            }
    
            propertyMetadata.Model = newPropertyValue;