global using System;
global using System.Threading.Tasks;
global using System.Collections.Generic;
global using System.Linq;
global using System.Collections;
global using System.Threading;
global using Microsoft.AspNetCore.Http;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Options;
global using Microsoft.Extensions.DependencyInjection.Extensions;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Logging.Abstractions;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.Extensions.Primitives;

global using Meow.Helper;
global using Meow.Extension;
global using Meow.Dependency;
global using Meow.Config;
global using Meow.Tenant.Resolver;
global using Meow.Tenant.Middleware;
global using Meow.Tenant.Manager;

global using MeowISession = Meow.Security.Session.ISession;
global using MeowDomainTenantResolver = Meow.Tenant.Resolver.DomainTenantResolver;
