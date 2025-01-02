using System;
using System.Windows.Input;

namespace CarRentService.Common;

public class RelayCommand : ICommand
{
    private readonly Action _execute;
    private readonly Func<bool> _canExecute;

    // Конструктор для команды с возможностью проверки выполнения
    public RelayCommand(Action execute, Func<bool> canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute ?? (() => true); // Если не передан canExecute, команда всегда доступна для выполнения
    }

    // Событие, которое UI будет использовать для обновления доступности команды (например, активация/деактивация кнопки)
    public event EventHandler CanExecuteChanged;

    // Проверка, можно ли выполнить команду (например, кнопка будет активной, если эта функция возвращает true)
    public bool CanExecute(object parameter) => _canExecute();

    // Выполнение команды
    public void Execute(object parameter) => _execute();

    // Метод для вызова события CanExecuteChanged вручную (например, когда меняются условия, при которых команда может быть выполнена)
    public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}