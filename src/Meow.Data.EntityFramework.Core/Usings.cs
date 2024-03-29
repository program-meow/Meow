﻿global using System;
global using System.Threading.Tasks;
global using System.Collections.Generic;
global using System.Linq;
global using System.Threading;
global using System.Text;
global using System.Reflection;
global using System.Text.Json;
global using System.Linq.Expressions;
global using System.Text.Encodings.Web;
global using System.Text.Json.Serialization;
global using System.Text.Unicode;
global using System.IO;
global using System.ComponentModel.DataAnnotations.Schema;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Logging.Abstractions;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.ChangeTracking;
global using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
global using Microsoft.EntityFrameworkCore.Design;
global using Microsoft.Extensions.Configuration;
global using Microsoft.EntityFrameworkCore.Metadata;
global using Microsoft.EntityFrameworkCore.Infrastructure;

global using SystemType = System.Type;
global using SystemAction = System.Action;
global using SystemException = System.Exception;

global using Meow.Helper;
global using Meow.Action;
global using Meow.Exception;
global using Meow.Model;
global using Meow.Domain.Operation;
global using Meow.Tenant;
global using Meow.Extension;
global using Meow.Dependency;
global using Meow.Query;
global using Meow.Json.Converter;
global using Meow.Security.Session;
global using Meow.Infrastructure;
global using Meow.Event;
global using Meow.Domain.Tree;
global using Meow.Domain.Extending;
global using Meow.Domain.Entity;
global using Meow.Domain.Repository;
global using Meow.Domain.Auditing;
global using Meow.Domain.Event;
global using Meow.Domain.Compare;
global using Meow.Date;
global using Meow.Data.Filter;
global using Meow.Data.Query;
global using Meow.Data.Store;
global using Meow.Data.EntityFrameworkCore.Filter;
global using Meow.Data.EntityFrameworkCore.ValueComparer;
global using Meow.Data.EntityFrameworkCore.ValueConverter;

