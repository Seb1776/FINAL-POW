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

    public Student? GetStudentById(ObjectId id)
    {
        return _studentDbContext.Students.FirstOrDefault(s => s.Id == id
        );
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
        var studentToDelete = _studentDbContext.Students.FirstOrDefault(s => s.Id == student.Id);

        if (studentToDelete != null)
        {
            _studentDbContext.Students.Remove(studentToDelete);
            _studentDbContext.ChangeTracker.DetectChanges();
            Console.WriteLine(_studentDbContext.ChangeTracker.DebugView.LongView);
            _studentDbContext.SaveChanges();
        }

        else
            throw new AggregateException("Student to delete cannot be found");
    }

    public void UpdateStudent(Student student)
    {
        var studentToUpdate = _studentDbContext.Students.FirstOrDefault(s => s.Id == student.Id);

        if (studentToUpdate != null)
        {
            studentToUpdate.username = student.username;
            studentToUpdate.studentLastName = student.studentLastName;
            studentToUpdate.password = student.password;
            studentToUpdate.studentName = student.studentName;

            _studentDbContext.Students.Update(studentToUpdate);
            
            _studentDbContext.ChangeTracker.DetectChanges();
            Console.WriteLine(_studentDbContext.ChangeTracker.DebugView.LongView);

            _studentDbContext.SaveChanges();
        }

        else
        {
            throw new ArgumentException("Student to update cannot be found");
        }
    }
}