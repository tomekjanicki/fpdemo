namespace Demo.Common.IoC.Interfaces
{
    public interface IIoCContainer
    {
        T Get<T>()
            where T : class;
    }
}
