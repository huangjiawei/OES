use examination;
ALTER DATABASE examination set ENABLE_BROKER ;
restore master key from file='F:\My File\��ͬ��\�ҵ��ĵ�\OnlineCertification\App_Data\ExaminationMasterKey'
decryption by password='951248'
encryption by password='3edc4rfv';
open master key decryption by password='3edc4rfv';
alter master key add encryption by service master key;