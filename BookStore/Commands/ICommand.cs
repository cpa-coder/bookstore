using BookStore.Models;

namespace BookStore.Commands;

public interface ICommand<T>
{
   T? Execute(string[] args);
}