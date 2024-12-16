using vm.src.Utils;
using vm.src.Utils.Enums;

namespace vm.src;

public static class InstructionRegistry 
{
    public static readonly List<VmInstruction> Instructions = 
    [
        new() {
            Instr = "push",
            OpCode = 1,
            ExpectedArgCount = 1,
            ExpectedStackArgCount = 0,
            Errors = [
                VmError.ExpectedArgument
            ]
        },
        new() {
            Instr = "pop",
            OpCode = 2,
            ExpectedArgCount = 0,
            ExpectedStackArgCount = 1,
            Errors = [
                VmError.ExpectedStackArgument
            ]
        },
        new() {
            Instr = "add",
            OpCode = 3,
            ExpectedArgCount = 0,
            ExpectedStackArgCount = 2,
            Errors = [
                VmError.ExpectedStackArgument
            ]
        },
        new() {
            Instr = "sub",
            OpCode = 4,
            ExpectedArgCount = 0,
            ExpectedStackArgCount = 2,
            Errors = [
                VmError.ExpectedStackArgument
            ]
        },
        new() {
            Instr = "mul",
            OpCode = 5,
            ExpectedArgCount = 0,
            ExpectedStackArgCount = 2,
            Errors = [
                VmError.ExpectedStackArgument
            ]
        },
        new() {
            Instr = "div",
            OpCode = 6,
            ExpectedArgCount = 0,
            ExpectedStackArgCount = 2,
            Errors = [
                VmError.ExpectedStackArgument
            ]
        },
    ];

    public static VmInstruction? Instr(string instr) 
    {
        return Instructions.FirstOrDefault(x => x.Instr == instr);
    }

    public static VmInstruction? Instr(int opCode) 
    {
        return Instructions.FirstOrDefault(x => x.OpCode == opCode);
    }
}