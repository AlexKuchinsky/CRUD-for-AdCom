USE Enrollment

SELECT Specialities.SpecialityId, EducationPlaces.Name AS University, NCSQSpecialities.Name AS Speciality, EducationForms.Name AS Form
FROM Specialities 
JOIN EducationForms ON Specialities.EducationFormId = EducationForms.EducationFormId
JOIN EducationPlaces ON Specialities.EducationPlaceId = EducationPlaces.EducationPlaceId
JOIN NCSQSpecialities ON Specialities.NCSQSpecialityId = NCSQSpecialities.NCSQSpecialityId