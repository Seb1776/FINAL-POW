using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using POWAPI.Models;

namespace POWAPI.Services;

public class StudentService : IStudentService
{
    private readonly StudentDbContext _studentDbContext;

    public StudentService(StudentDbContext studentDbContext)
    {
        _studentDbContext = studentDbContext;
    }

    public IEnumerable<Student> GetAllStudents()
    {
        return _studentDbContext.Students.OrderByDescending(s => s.Id)
            .Take(20).AsNoTracking().AsEnumerable();
    }

    public Student? GetStudentById(string id)
    {
        return _studentDbContext.Students.FirstOrDefault(s => s.Id.Equals(id));
    }

    public Student? GetStudentById(ObjectId id)
    {
        return GetStudentById(id.ToString());
    }

    public void AddStudent(Student student)
    {
        _studentDbContext.Students.Add(student);
        
        _studentDbContext.ChangeTracker.DetectChanges();
        Console.WriteLine(_studentDbContext.ChangeTracker.DebugView.LongView);

        _studentDbContext.SaveChanges();
    }

    public void DeleteStudent(Student student)
    {
        _studentDbContext.Remove(student);
        _studentDbContext.ChangeTracker.DetectChanges();
        Console.WriteLine(_studentDbContext.ChangeTracker.DebugView.LongView);
        _studentDbContext.SaveChanges();
    }

    public void DeleteStudent(string id)
    {
        var studentToDelete = _studentDbContext.Students.FirstOrDefault(s => s.Id.Equals(id));
            
        if (studentToDelete != null)
            DeleteStudent(studentToDelete);

        else
            throw new ArgumentException("Student to delete cannot be found");
    }

    public void UpdateStudent(Student student)
    {
        var studentToUpdate = new Student
        {
            username = student.username,
            password = student.password,
            studentName = student.studentName,
            studentLastName = student.studentLastName
        };

        _studentDbContext.Students.Update(studentToUpdate);
        _studentDbContext.ChangeTracker.DetectChanges();
        Console.WriteLine(_studentDbContext.ChangeTracker.DebugView.LongView);
        _studentDbContext.SaveChanges();
        
        //var studentToUpdate = _studentDbContext.Students.FirstOrDefault(s => s.Id == id);
    }

    public void UpdateStudent(string id)
    {
        var studentToUpdate = _studentDbContext.Students.FirstOrDefault(s => s.Id.Equals(id));
            
        if (studentToUpdate != null)
            UpdateStudent(studentToUpdate);

        else
            throw new ArgumentException("Student to update cannot be found");
    }
}