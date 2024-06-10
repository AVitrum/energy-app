namespace UserApi.Exceptions;

public class UserException : Exception
{
    public UserException() : base() { }
    protected UserException(string message) : base(message) { }
    protected UserException(string message, Exception inner) : base(message, inner) { }
}

public class UserExistsByEmailException : UserException
{
    public UserExistsByEmailException() : base("User with the provided email exists.") { }
    public UserExistsByEmailException(string message) : base(message) { }
    public UserExistsByEmailException(string message, Exception inner) : base(message, inner) { }
}

public class UserWrongPasswordException : UserException
{
    public UserWrongPasswordException() : base("Password is incorrect.") { }
    public UserWrongPasswordException(string message) : base(message) { }
    public UserWrongPasswordException(string message, Exception inner) : base(message, inner) { }
}