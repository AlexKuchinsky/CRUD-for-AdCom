USE Enrollment

ALTER TABLE ApplicationToSpecialities
ADD CONSTRAINT UQ_ApplicationId_Priority UNIQUE(ApplicationId, Priority)