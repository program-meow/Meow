/*==============================================================*/
/* Systems  系统                                                */
/*==============================================================*/

CREATE USER "Systems"
IDENTIFIED BY system;

/*==============================================================*/
/* Application  应用程序                                        */
/*==============================================================*/

create table "Systems"."Application" (
   "ApplicationId"      RAW(36)              not null,
   "Code"               NVARCHAR2(60)         not null,
   "Name"               NVARCHAR2(200)        not null,
   "Comment"            NVARCHAR2(500),
   "Enabled"            SMALLINT              not null,
   "RegisterEnabled"    SMALLINT              not null,
   "CreationTime"       DATE                  not null,
   "CreatorId"          RAW(36),
   "LastModificationTime" DATE                  not null,
   "LastModifierId"     RAW(36),
   "IsDeleted"          SMALLINT              not null,
   "Version"            RAW(72),
   constraint PK_APPLICATION primary key ("ApplicationId")
);
