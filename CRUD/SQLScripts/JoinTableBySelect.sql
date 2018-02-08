USE Enrollment

SELECT * FROM SubjectMarks, Subjects
WHERE (SubjectMarks.SubjectId = Subjects.SubjectId)