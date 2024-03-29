﻿global using System;
global using System.Threading.Tasks;
global using System.Collections.Generic;
global using System.Linq;
global using System.ComponentModel.DataAnnotations;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.DependencyInjection.Extensions;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

global using Meow.Model;
global using Meow.Validation;
global using Meow.Exception;
global using Meow.Helper;
global using Meow.Aop;
global using Meow.Config;
global using Meow.Caching;
global using Meow.Extension;
global using Meow.Dependency;
global using Meow.Event;
global using Meow.Logging;
global using Meow.Query;
global using Meow.Application.Dto;
global using Meow.Security.Session;
global using Microsoft.Extensions.Localization;

global using SystemException = System.Exception;
