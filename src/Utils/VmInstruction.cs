using vm.src.Utils.Enums;

namespace vm.src.Utils;

public sealed class VmInstruction
{
    public string Instr { get; init; } = "NONE";
    public int OpCode { get; init; }
    public int ExpectedArgCount { get; init; }
    public int ExpectedStackArgCount { get; init; }
    public List<VmError> Errors { get; init; } = [];

    public bool HasError(VmError error) {
        return Errors.Contains(error);
    }
}