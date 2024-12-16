using vm.src;
using vm.src.Utils;

namespace vm.src.Lexer;

public sealed class VmLexer(string source) {
    private readonly string Source = source;
    private int Current = 0;
    private readonly List<int> tokens = [];

    public VmResult<List<int>> Tokenize() {
        while (!IsEnd()) {
            var result = ParseToken();

            if (result.Error is not null) 
                return VmResult<List<int>>.Err(result.Error);
            
            Current++;
        }

        return VmResult<List<int>>.Ok(tokens);
    }

    private VmResult ParseToken() {
        return Source[Current] switch {
            _ when char.IsAsciiLetter(Source[Current]) => ParseIdentifier(),
            _ when char.IsAsciiDigit(Source[Current]) => ParseNumber(),
            '\n' or '\r' or ' ' or '\t' => VmResult.Ok(),
            _ => VmResult.Err(VmErrorType.UnknownToken(Source[Current].ToString()))
        };
    }

    private VmResult ParseNumber() {
        int start = Current;

        while (!IsEnd() && char.IsAsciiDigit(Source[Current]))
            Current++;

        var lexeme = Source[start..Current];
        tokens.Add(int.Parse(lexeme));

        return VmResult.Ok();
    }

    private VmResult ParseIdentifier() {
        int start = Current;

        while (!IsEnd() && char.IsAsciiLetter(Source[Current]))
            Current++;

        var lexeme = Source[start..Current];

        var instr = InstructionRegistry.Instr(lexeme);
        if (instr is null) {
            return VmResult.Err(VmErrorType.UndefinedInstruction());
        }

        tokens.Add(instr.OpCode);
        return VmResult.Ok();
    }

    private bool IsEnd() {
        return Current >= Source.Length;
    }
}