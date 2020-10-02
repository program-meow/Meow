/*==============================================================*/
/* Systems  系统                                                */
/*==============================================================*/


/*==============================================================*/
/* Application  应用程序                                        */
/*==============================================================*/


--删除表中所有外键
DECLARE @SQL VARCHAR(max),@FK VARCHAR(max),@TB varchar(max)
set @TB= 'Systems.Application'
DECLARE CUR_FK CURSOR LOCAL FOR
SELECT OBJECT_NAME(CONSTID) FROM SYSREFERENCES where FKEYID=object_id(@TB)
OPEN CUR_FK
FETCH CUR_FK INTO @FK
WHILE @@FETCH_STATUS =0
BEGIN
SELECT @SQL='ALTER TABLE '+@TB+' DROP CONSTRAINT '+@FK
exec(@SQL)
FETCH CUR_FK INTO @FK
END
CLOSE CUR_FK
/*==============================================================*/
/* Table: Application                                           */
/*==============================================================*/
/*==============================================================*/
/* 建表*/
/*==============================================================*/
if not exists (select 1 from  sys.tables
           where object_id = object_id('Systems.Application')  and type='U')
begin
create table Systems.Application (
   ApplicationId        uniqueidentifier     not null,
   Code                 nvarchar(60)         not null,
   Name                 nvarchar(200)        not null,
   Comment              nvarchar(500)        null,
   Enabled              bit                  not null,
   RegisterEnabled      bit                  not null,
   CreationTime         datetime             not null,
   CreatorId            uniqueidentifier     null,
   LastModificationTime datetime             not null,
   LastModifierId       uniqueidentifier     null,
   IsDeleted            bit                  not null,
   Version              timestamp            null,
   constraint PK_APPLICATION primary key nonclustered (ApplicationId)
)
end
go

/*==============================================================*/
/* 添加表描述*/
/*==============================================================*/
if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Systems.Application') and minor_id = 0)
begin 
   execute sp_dropextendedproperty 'MS_Description',  
   'schema', 'Systems', 'table', 'Application' 
 
end 


execute sp_addextendedproperty 'MS_Description',  
   '应用程序', 
   'schema', 'Systems', 'table', 'Application'
go

/*==============================================================*/
/* 列不存在即添加列*/
/*==============================================================*/
if not exists(
select 1 from  sys.columns
           where object_id = object_id('Systems.Application')  and name='ApplicationId')
begin
alter table Systems.Application
   add ApplicationId uniqueidentifier     not null
   
end

/*==============================================================*/
/* 描述删除再添加*/
/*==============================================================*/
if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Systems.Application')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'ApplicationId')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'schema', 'Systems', 'table', 'Application', 'column', 'ApplicationId'

end


execute sp_addextendedproperty 'MS_Description', 
   '应用程序编号',
   'schema', 'Systems', 'table', 'Application', 'column', 'ApplicationId'
go

/*==============================================================*/
/* 列不存在即添加列*/
/*==============================================================*/
if not exists(
select 1 from  sys.columns
           where object_id = object_id('Systems.Application')  and name='Code')
begin
alter table Systems.Application
   add Code nvarchar(60)         not null
   
end

/*==============================================================*/
/* 描述删除再添加*/
/*==============================================================*/
if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Systems.Application')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Code')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'schema', 'Systems', 'table', 'Application', 'column', 'Code'

end


execute sp_addextendedproperty 'MS_Description', 
   '应用程序编码',
   'schema', 'Systems', 'table', 'Application', 'column', 'Code'
go

/*==============================================================*/
/* 列不存在即添加列*/
/*==============================================================*/
if not exists(
select 1 from  sys.columns
           where object_id = object_id('Systems.Application')  and name='Name')
begin
alter table Systems.Application
   add Name nvarchar(200)        not null
   
end

/*==============================================================*/
/* 描述删除再添加*/
/*==============================================================*/
if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Systems.Application')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Name')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'schema', 'Systems', 'table', 'Application', 'column', 'Name'

end


execute sp_addextendedproperty 'MS_Description', 
   '应用程序名称',
   'schema', 'Systems', 'table', 'Application', 'column', 'Name'
go

/*==============================================================*/
/* 列不存在即添加列*/
/*==============================================================*/
if not exists(
select 1 from  sys.columns
           where object_id = object_id('Systems.Application')  and name='Comment')
begin
alter table Systems.Application
   add Comment nvarchar(500)        null
   
end

/*==============================================================*/
/* 描述删除再添加*/
/*==============================================================*/
if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Systems.Application')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Comment')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'schema', 'Systems', 'table', 'Application', 'column', 'Comment'

end


execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'schema', 'Systems', 'table', 'Application', 'column', 'Comment'
go

/*==============================================================*/
/* 列不存在即添加列*/
/*==============================================================*/
if not exists(
select 1 from  sys.columns
           where object_id = object_id('Systems.Application')  and name='Enabled')
begin
alter table Systems.Application
   add Enabled bit                  not null
   
end

/*==============================================================*/
/* 描述删除再添加*/
/*==============================================================*/
if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Systems.Application')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Enabled')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'schema', 'Systems', 'table', 'Application', 'column', 'Enabled'

end


execute sp_addextendedproperty 'MS_Description', 
   '启用',
   'schema', 'Systems', 'table', 'Application', 'column', 'Enabled'
go

/*==============================================================*/
/* 列不存在即添加列*/
/*==============================================================*/
if not exists(
select 1 from  sys.columns
           where object_id = object_id('Systems.Application')  and name='RegisterEnabled')
begin
alter table Systems.Application
   add RegisterEnabled bit                  not null
   
end

/*==============================================================*/
/* 描述删除再添加*/
/*==============================================================*/
if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Systems.Application')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'RegisterEnabled')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'schema', 'Systems', 'table', 'Application', 'column', 'RegisterEnabled'

end


execute sp_addextendedproperty 'MS_Description', 
   '启用注册',
   'schema', 'Systems', 'table', 'Application', 'column', 'RegisterEnabled'
go

/*==============================================================*/
/* 列不存在即添加列*/
/*==============================================================*/
if not exists(
select 1 from  sys.columns
           where object_id = object_id('Systems.Application')  and name='CreationTime')
begin
alter table Systems.Application
   add CreationTime datetime             not null
   
end

/*==============================================================*/
/* 描述删除再添加*/
/*==============================================================*/
if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Systems.Application')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreationTime')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'schema', 'Systems', 'table', 'Application', 'column', 'CreationTime'

end


execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'schema', 'Systems', 'table', 'Application', 'column', 'CreationTime'
go

/*==============================================================*/
/* 列不存在即添加列*/
/*==============================================================*/
if not exists(
select 1 from  sys.columns
           where object_id = object_id('Systems.Application')  and name='CreatorId')
begin
alter table Systems.Application
   add CreatorId uniqueidentifier     null
   
end

/*==============================================================*/
/* 描述删除再添加*/
/*==============================================================*/
if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Systems.Application')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'CreatorId')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'schema', 'Systems', 'table', 'Application', 'column', 'CreatorId'

end


execute sp_addextendedproperty 'MS_Description', 
   '创建人编号',
   'schema', 'Systems', 'table', 'Application', 'column', 'CreatorId'
go

/*==============================================================*/
/* 列不存在即添加列*/
/*==============================================================*/
if not exists(
select 1 from  sys.columns
           where object_id = object_id('Systems.Application')  and name='LastModificationTime')
begin
alter table Systems.Application
   add LastModificationTime datetime             not null
   
end

/*==============================================================*/
/* 描述删除再添加*/
/*==============================================================*/
if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Systems.Application')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LastModificationTime')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'schema', 'Systems', 'table', 'Application', 'column', 'LastModificationTime'

end


execute sp_addextendedproperty 'MS_Description', 
   '最后修改时间',
   'schema', 'Systems', 'table', 'Application', 'column', 'LastModificationTime'
go

/*==============================================================*/
/* 列不存在即添加列*/
/*==============================================================*/
if not exists(
select 1 from  sys.columns
           where object_id = object_id('Systems.Application')  and name='LastModifierId')
begin
alter table Systems.Application
   add LastModifierId uniqueidentifier     null
   
end

/*==============================================================*/
/* 描述删除再添加*/
/*==============================================================*/
if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Systems.Application')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'LastModifierId')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'schema', 'Systems', 'table', 'Application', 'column', 'LastModifierId'

end


execute sp_addextendedproperty 'MS_Description', 
   '最后修改人编号',
   'schema', 'Systems', 'table', 'Application', 'column', 'LastModifierId'
go

/*==============================================================*/
/* 列不存在即添加列*/
/*==============================================================*/
if not exists(
select 1 from  sys.columns
           where object_id = object_id('Systems.Application')  and name='IsDeleted')
begin
alter table Systems.Application
   add IsDeleted bit                  not null
   
end

/*==============================================================*/
/* 描述删除再添加*/
/*==============================================================*/
if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Systems.Application')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'IsDeleted')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'schema', 'Systems', 'table', 'Application', 'column', 'IsDeleted'

end


execute sp_addextendedproperty 'MS_Description', 
   '是否删除',
   'schema', 'Systems', 'table', 'Application', 'column', 'IsDeleted'
go

/*==============================================================*/
/* 列不存在即添加列*/
/*==============================================================*/
if not exists(
select 1 from  sys.columns
           where object_id = object_id('Systems.Application')  and name='Version')
begin
alter table Systems.Application
   add Version timestamp            null
   
end

/*==============================================================*/
/* 描述删除再添加*/
/*==============================================================*/
if exists(select 1 from sys.extended_properties p where
      p.major_id = object_id('Systems.Application')
  and p.minor_id = (select c.column_id from sys.columns c where c.object_id = p.major_id and c.name = 'Version')
)
begin
   execute sp_dropextendedproperty 'MS_Description', 
   'schema', 'Systems', 'table', 'Application', 'column', 'Version'

end


execute sp_addextendedproperty 'MS_Description', 
   '版本号',
   'schema', 'Systems', 'table', 'Application', 'column', 'Version'
go
