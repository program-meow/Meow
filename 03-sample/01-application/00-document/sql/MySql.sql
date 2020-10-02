/*==============================================================*/
/* Systems  系统                                                */
/*==============================================================*/


/*==============================================================*/
/* Application  应用程序                                        */
/*==============================================================*/


drop table if exists `Systems.Application`;

/*==============================================================*/
/* Table: Application                                           */
/*==============================================================*/
create table `Systems.Application`
(
   ApplicationId        char(36) not null comment '应用程序编号',
   Code                 national varchar(60) not null comment '应用程序编码',
   Name                 national varchar(200) not null comment '应用程序名称',
   Comment              national varchar(500) comment '备注',
   Enabled              bool not null comment '启用',
   RegisterEnabled      bool not null comment '启用注册',
   CreationTime         datetime not null comment '创建时间',
   CreatorId            char(36) comment '创建人编号',
   LastModificationTime datetime not null comment '最后修改时间',
   LastModifierId       char(36) comment '最后修改人编号',
   IsDeleted            bool not null comment '是否删除',
   Version              tinyblob comment '版本号',
   primary key (ApplicationId)
);

alter table `Systems.Application` comment '应用程序';

/*==============================================================*/
/* Index: clus_idx_creationtime                                 */
/*==============================================================*/
create index clus_idx_creationtime on `Systems.Application`
(
   CreationTime
);
