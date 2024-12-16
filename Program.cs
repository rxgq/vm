using vm.src.Lexer;
using vm.src.Machine;

namespace vm;

internal class Program 
{
    static void Main()
    {
        var source = File.ReadAllText("example/source.txt");

        var lexer = new VmLexer(source);
        var program = lexer.Tokenize();

        if (program.Error is not null)
        {
            Console.WriteLine(program.Error.ToString());
            return;
        }

        var vm = new VirtualMachine(program.Value!);
        var result = vm.Run();

        Console.WriteLine(result.ToString());
    }
}

