/*==============================================================*/
/* Systems  ϵͳ                                                */
/*==============================================================*/


/*==============================================================*/
/* Application  Ӧ�ó���                                        */
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
'Ӧ�ó���';

comment on column "Systems"."Application"."ApplicationId" is
'Ӧ�ó�����';

comment on column "Systems"."Application"."Code" is
'Ӧ�ó������';

comment on column "Systems"."Application"."Name" is
'Ӧ�ó�������';

comment on column "Systems"."Application"."Comment" is
'��ע';

comment on column "Systems"."Application"."Enabled" is
'����';

comment on column "Systems"."Application"."RegisterEnabled" is
'����ע��';

comment on column "Systems"."Application"."CreationTime" is
'����ʱ��';

comment on column "Systems"."Application"."CreatorId" is
'�����˱��';

comment on column "Systems"."Application"."LastModificationTime" is
'����޸�ʱ��';

comment on column "Systems"."Application"."LastModifierId" is
'����޸��˱��';

comment on column "Systems"."Application"."IsDeleted" is
'�Ƿ�ɾ��';

comment on column "Systems"."Application"."Version" is
'�汾��';

-- set table ownership  alter table Systems.Application owner to Systems
;