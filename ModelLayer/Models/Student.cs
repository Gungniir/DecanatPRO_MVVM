using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ModelLayer.Models
{
    public class Student : IDomainObject, INotifyPropertyChanged
    {
        private readonly string[] _names = {
            "Владимир Курусь",
            "Иван Арляпов",
            "Иван Ванюшкин",
            "Константин Калинин",
            "Софья Качаева",
            "Татьяна Викторова",
            "Юлия Викторова",
            "Кирилл Нагель"
        };

        private readonly string[] _specialities =
        {
            "Прикладная информатика",
            "Программная инженерия",
        };

        private readonly string[] _groups =
        {
            "1",
            "2",
        };

        private int _id;
        private string _name = "";
        private string _speciality = "";
        private string _group = "";

        public int Id
        {
            get => _id;
            set => SetField(ref _id, value);
        }

        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }

        public string Speciality
        {
            get => _speciality;
            set => SetField(ref _speciality, value);
        }

        public string Group
        {
            get => _group;
            set => SetField(ref _group, value);
        }

        public void FillRandom(int seed)
        {
            Random random = new Random(seed);

            Name = _names[random.Next(_names.Length)];
            Speciality = _specialities[random.Next(_specialities.Length)];
            Group = _groups[random.Next(_groups.Length)];
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}