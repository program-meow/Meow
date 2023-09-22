﻿global using System;
global using System.Threading.Tasks;
global using System.Collections.Generic;
global using System.Linq;
global using System.Diagnostics;
global using System.IO;
global using System.Reflection;
global using System.Text.Json;
global using System.Text.Encodings.Web;
global using System.Text.Unicode;
global using System.Text.Json.Serialization;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.Extensions.Localization;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Mvc.ModelBinding;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Http;
global using SystemStream = System.IO.Stream;
global using SystemException = System.Exception;
global using SystemAction = System.Action;
global using IMeowSession = Meow.Authentication.Session.ISession;
global using Meow.Helper;
global using Meow.Model;
global using Meow.Dependency;
global using Meow.Extension;
global using Meow.Exception;
global using Meow.Response;
global using Meow.Logging;
global using Meow.Query;
global using Meow.Infrastructure;
global using Meow.Converter;
global using Meow.Authentication.Session;
global using Meow.Application.Dto;
global using Meow.Application.Model;
global using Meow.Application.Tree;
global using Meow.Application.Filter;
global using Meow.Application.Infrastructure;
global using Meow.Application.Extension;
global using Meow.Application.Lock;
global using Meow.Application.Logging;