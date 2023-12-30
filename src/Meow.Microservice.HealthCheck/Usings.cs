global using System;
global using System.Threading.Tasks;
global using System.Collections.Generic;
global using System.Linq;
global using System.Threading;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Diagnostics.HealthChecks;
global using Dapr.Client;

global using Meow.Data;
global using Meow.Data.EntityFrameworkCore;
global using Meow.Extension;

global using Meow.Microservice.HealthCheck.Dapr;
global using Meow.Microservice.HealthCheck.EntityFrameworkCore;