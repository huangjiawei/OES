USE EXAMINATION
GO


create master key encryption by password='3edc4rfv';

create asymmetric key AsyKey with algorithm=RSA_512

CREATE CERTIFICATE certKeyPublisher
WITH SUBJECT='Personal Data Encryption Certficate';

backup master key to file ='F:\My File\云同步\我的文档\OnlineCertification\App_Data\ExaminationMasterKey'
encryption by password='951248';

alter master key 

restore master key from file='F:\My File\云同步\我的文档\OnlineCertification\App_Data\ExaminationMasterKey'
decryption by password='951248'
encryption by password='3edc4rfv';

create symmetric key certificateKey
with algorithm=DES
encryption by Certificate certKeyPublisher;
