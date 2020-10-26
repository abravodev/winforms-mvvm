using System;

namespace UserManager.BusinessLogic.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {

        }

        public NotFoundException(string type, string parameter, object id) : this($"{type} not found with {parameter} = {id}")
        {

        }

        public NotFoundException(string type, object id) : this(type, "id", id)
        {

        }
    }

    public class NotFoundException<T> : NotFoundException
    {
        public NotFoundException(string message) : base(message)
        {

        }

        public NotFoundException(string parameter, object id) : base($"{typeof(T).Name} not found with {parameter} = {id}")
        {

        }

        public NotFoundException(object id) : base(typeof(T).Name, "id", id)
        {

        }
    }
}
