using vm.src.Utils;
using vm.src.Utils.Enums;

namespace vm.src.Machine;

public sealed class VirtualMachine(List<int> program)
{
    private readonly List<int> Program = program;
    private int Ip = 0;

    private readonly Stack<int> Stack = new();

    public VmResult Run()
    {
        while (Ip < Program.Count) {
            var result = Execute();

            if (!result.IsSuccess) return result;
            Ip++;
        }

        Dump();
        return VmResult.Ok();
    }

    private VmResult Execute() {
        var result = CheckInstructionConstraints();
        if (!result.IsSuccess) return result;

        _ = Program[Ip] switch {
            0x1 => ExecPush(),
            0x2 => ExecPop(),
            0x3 => ExecAdd(),
            0x4 => ExecSub(),
            0x5 => ExecMul(),
            0x6 => ExecDiv(),
            _   => VmResult.Err(VmErrorType.UndefinedInstruction())
        };

        return VmResult.Ok();
    }

    private VmResult CheckInstructionConstraints() {
        var inst = InstructionRegistry.Instr(Program[Ip]);

        if (inst is null) {
            return VmResult.Err(VmErrorType.UndefinedInstruction());
        }

        if (inst.HasError(VmError.ExpectedArgument)) {
            if (Ip == Program.Count - 1) {
                return VmResult.Err(VmErrorType.ExpectedArgument(inst.Instr, inst.ExpectedArgCount));
            }
        }

        if (inst.HasError(VmError.StackUnderflow)) {
            if (Stack.Count == 0) {
                return VmResult.Err(VmErrorType.StackUnderflow());
            }
        }

        if (inst.HasError(VmError.ExpectedStackArgument)) {
            if (Stack.Count < inst.ExpectedStackArgCount) {
                return VmResult.Err(VmErrorType.ExpectedStackValue(inst.Instr, inst.ExpectedStackArgCount));
            }
        }

        return VmResult.Ok();
    }

    private void Dump() {
        Console.WriteLine("\nSTACK:");
        foreach (var i in Stack) {
            Console.WriteLine($"  {i}");
        }

        Console.WriteLine("\n");
    }

    private VmResult ExecPush() {
        Ip++;
        Stack.Push(Program[Ip]);

        return VmResult.Ok();
    }

    private VmResult ExecPop() {
        Stack.Pop();

        return VmResult.Ok();
    }

    private VmResult ExecAdd() {
        var a = Stack.Pop();
        var b = Stack.Pop();

        Stack.Push(a + b);

        return VmResult.Ok();
    }

    private VmResult ExecSub() {
        var b = Stack.Pop();
        var a = Stack.Pop();

        Stack.Push(a - b);

        return VmResult.Ok();
    }

    private VmResult ExecMul() {
        var b = Stack.Pop();
        var a = Stack.Pop();

        Stack.Push(a * b);

        return VmResult.Ok();
    }

    private VmResult ExecDiv() {
        var b = Stack.Pop();
        if (b == 0) return VmResult.Err(VmErrorType.DivideByZero());
        var a = Stack.Pop();

        Stack.Push(a / b);

        return VmResult.Ok();
    }


}