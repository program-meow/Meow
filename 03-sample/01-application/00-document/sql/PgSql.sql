/*==============================================================*/
/* Systems  系统                                                */
/*==============================================================*/


/*==============================================================*/
/* Application  应用程序                                        */
/*==============================================================*/


/*==============================================================*/
/* Table: Application                                           */
/*==============================================================*/
create table "Systems"."Application" (
   "ApplicationId" UUID                 not null,
   "Code" VARCHAR(60)          not null,
   "Name" VARCHAR(200)         not null,
   "Comment" VARCHAR(500)         null,
   "Enabled" BOOL                 not null,
   "RegisterEnabled" BOOL                 not null,
   "CreationTime" TIMESTAMP            not null,
   "CreatorId" UUID                 null,
   "LastModificationTime" TIMESTAMP            not null,
   "LastModifierId" UUID                 null,
   "IsDeleted" BOOL                 not null,
   "Version" BYTEA                null,
   constraint PK_APPLICATION primary key ("ApplicationId")
);

comment on table "Systems"."Application" is
'应用程序';

comment on column "Systems"."Application"."ApplicationId" is
'应用程序编号';

comment on column "Systems"."Application"."Code" is
'应用程序编码';

comment on column "Systems"."Application"."Name" is
'应用程序名称';

comment on column "Systems"."Application"."Comment" is
'备注';

comment on column "Systems"."Application"."Enabled" is
'启用';

comment on column "Systems"."Application"."RegisterEnabled" is
'启用注册';

comment on column "Systems"."Application"."CreationTime" is
'创建时间';

comment on column "Systems"."Application"."CreatorId" is
'创建人编号';

comment on column "Systems"."Application"."LastModificationTime" is
'最后修改时间';

comment on column "Systems"."Application"."LastModifierId" is
'最后修改人编号';

comment on column "Systems"."Application"."IsDeleted" is
'是否删除';

comment on column "Systems"."Application"."Version" is
'版本号';

-- set table ownership  alter table Systems.Application owner to Systems
;