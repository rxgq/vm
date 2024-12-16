namespace vm.src.Utils;

public class VmErrorType(string code, string? message)
{
    public readonly string Code = code;
    public readonly string? Message = message;

    public override string ToString()
    {
        return $"{Code}: {Message ?? "No details."}";
    }

    public static VmErrorType UndefinedInstruction() {
        return new("UNKNOWN_INSTRUCTION", null);
    }

    public static VmErrorType UnknownToken(string token) {
        return new("UNKNOWN_TOKEN", $"'{token}'");
    }
    
    public static VmErrorType ExpectedArgument(string inst, int count) {
        return new("EXPECTED_ARGUMENT", $"{inst} expects {count} value argument(s).");
    }

    public static VmErrorType ExpectedStackValue(string inst, int count) {
        return new("EXPECTED_STACK_VALUE", $"{inst} expects {count} value(s) to be on the stack.");
    }

    public static VmErrorType StackUnderflow() {
        return new("STACK_UNDERFLOW", "The number of items on the stack was -1.");
    }
    
    public static VmErrorType DivideByZero() {
        return new("STACK_UNDERFLOW", "The number of items on the stack was -1.");
    }
}