using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScheduleOfClasses_Diplom.Data;

namespace ScheduleOfClasses_Diplom.Data
{
    public class School
    {
        ObservableCollection<SchoolClass> _Classes;

        public School()
        {
            _Classes = new ObservableCollection<SchoolClass>();
        }

        public ObservableCollection<SchoolClass> GetClasses()
        {
            return _Classes;
        }

        public void AddClass(string name, string teacherName, string roomName)
        {
            _Classes.Add( new SchoolClass
            {
                _Name = name,
                _TeacherName = teacherName,
                _RoomName = roomName
            });
        }
        public void AddClass(SchoolClass schoolClass)
        {
            _Classes.Add(new SchoolClass
            {
                _Name = schoolClass._Name,
                _TeacherName = schoolClass._TeacherName,
                _RoomName = schoolClass._RoomName
            });
        }

    }
}
