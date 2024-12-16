namespace vm.src.Utils;

public sealed class VmResult
{
    public readonly bool IsSuccess;
    public readonly VmErrorType? Error;

    private VmResult(bool isSuccess, VmErrorType? error) {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static VmResult Ok() {
        return new(true, null);
    }

    public static VmResult Err(VmErrorType err) {
        return new(false, err);
    }

    public override string ToString() {
        if (Error is not null) {
            return Error.ToString();
        }

        return "Execution Finished.";
    }
}

public sealed class VmResult<T>
{
    public readonly T? Value;
    public readonly bool IsSuccess;
    public readonly VmErrorType? Error;

    private VmResult(T? value, bool isSuccess, VmErrorType? error) {
        Value = value;
        IsSuccess = isSuccess;
        Error = error;
    }

    public static VmResult<T> Ok(T t) {
        return new(t, true, null);
    }

    public static VmResult<T> Err(VmErrorType err) {
        return new(default, false, err);
    }

    public override string ToString() {
        if (Error is not null) {
            return Error.ToString();
        }

        return "Execution Finished.";
    }
}