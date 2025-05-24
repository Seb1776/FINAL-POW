using MongoDB.Bson;
using POWAPI.Models;

namespace POWAPI.Services;

public interface IStudentService
{
    IEnumerable<Student> GetAllStudents();
    Student? GetStudentById(ObjectId id);
    void AddStudent(Student newStudent);
    void UpdateStudent(Student updatedStudent);
    void UpdateStudent(string id);
    void DeleteStudent(Student studentToDelete);
    void DeleteStudent(string id);
}