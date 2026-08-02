#pragma warning disable CS1591 // Experimental diagnostics surface; not proposed as final API.

using System.Collections.Generic;

namespace AngleSharp.Html.Parser.Utf8;

using System;

public sealed class Utf8HtmlTokenizerStateMetrics(Int32 stateCount)
{
    private readonly Int64[] _byteVisits = new Int64[stateCount];
    private readonly Int64[] _runs = new Int64[stateCount];
    private readonly Int32[] _maximumRunLengths = new Int32[stateCount];
    private Int32 _lastState = -1;
    private Int32 _currentRunLength;

    public void Record(Int32 state, Int32 byteCount)
    {
        if (byteCount == 0)
        {
            return;
        }

        _byteVisits[state] += byteCount;
        if (_lastState == state)
        {
            _currentRunLength += byteCount;
        }
        else
        {
            FinishRun();
            _lastState = state;
            _currentRunLength = byteCount;
            _runs[state]++;
        }

        if (_currentRunLength > _maximumRunLengths[state])
        {
            _maximumRunLengths[state] = _currentRunLength;
        }
    }

    public IReadOnlyList<Utf8HtmlTokenizerStateMetric> Snapshot(IReadOnlyList<String> stateNames)
    {
        var result = new List<Utf8HtmlTokenizerStateMetric>();
        for (var state = 0; state < _byteVisits.Length; state++)
        {
            if (_byteVisits[state] == 0)
            {
                continue;
            }

            result.Add(
                new Utf8HtmlTokenizerStateMetric(
                    stateNames[state],
                    _byteVisits[state],
                    _runs[state],
                    _maximumRunLengths[state]
                )
            );
        }

        result.Sort(static (left, right) => right.ByteVisits.CompareTo(left.ByteVisits));
        return result;
    }

    private void FinishRun()
    {
        if (_lastState >= 0 && _currentRunLength > _maximumRunLengths[_lastState])
        {
            _maximumRunLengths[_lastState] = _currentRunLength;
        }
    }
}

public readonly record struct Utf8HtmlTokenizerStateMetric(
    String State,
    Int64 ByteVisits,
    Int64 Runs,
    Int32 MaximumRunLength
);
