/*==============================================================*/
/* Systems  ϵͳ                                                */
/*==============================================================*/


/*==============================================================*/
/* Application  Ӧ�ó���                                        */
/*==============================================================*/


drop table if exists `Systems.Application`;

/*==============================================================*/
/* Table: Application                                           */
/*==============================================================*/
create table `Systems.Application`
(
   ApplicationId        char(36) not null comment 'Ӧ�ó�����',
   Code                 national varchar(60) not null comment 'Ӧ�ó������',
   Name                 national varchar(200) not null comment 'Ӧ�ó�������',
   Comment              national varchar(500) comment '��ע',
   Enabled              bool not null comment '����',
   RegisterEnabled      bool not null comment '����ע��',
   CreationTime         datetime not null comment '����ʱ��',
   CreatorId            char(36) comment '�����˱��',
   LastModificationTime datetime not null comment '����޸�ʱ��',
   LastModifierId       char(36) comment '����޸��˱��',
   IsDeleted            bool not null comment '�Ƿ�ɾ��',
   Version              tinyblob comment '�汾��',
   primary key (ApplicationId)
);

alter table `Systems.Application` comment 'Ӧ�ó���';

/*==============================================================*/
/* Index: clus_idx_creationtime                                 */
/*==============================================================*/
create index clus_idx_creationtime on `Systems.Application`
(
   CreationTime
);
