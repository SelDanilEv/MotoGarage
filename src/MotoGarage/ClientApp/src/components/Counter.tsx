import * as React from 'react';

interface CounterProps {
    multiplicator: number;
}

const Counter = ({ multiplicator } : CounterProps) => {
    const [count, setCount] = React.useState(0);
    const [count2, setCount2] = React.useState(0);

    const setCount3 = () => {
        setCount(count * multiplicator + 1);
    }

    return (
        <>
            <h1>Counter</h1>

            <p>This is a simple example of a React component. V2</p>

            <p aria-live="polite">Current count: <strong>{count}</strong></p>
            <p aria-live="polite">Current count2: <strong>{count2}</strong></p>

            <button type="button"
                className="btn btn-primary btn-lg"
                onClick={setCount3}>
                Increment
                </button>
            <button type="button"
                className="btn btn-primary btn-lg"
                onClick={() => setCount2(count2 + 1)}>
                Increment2
                </button>
        </>
    );
}

export default Counter;
