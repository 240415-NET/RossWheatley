import React, { useState } from 'react';

function FunctionalCounter() {

    const [count, setCount] = useState(0);

    const increment = () => setCount(count + 1);

    const decrement = () => setCount(count - 1);

    return (
        <div>
            <h3>This is my functional counting component</h3>
            
            {/* This is a comment block */}

            <br />

            <p>Count: {count}</p>
            <button onClick={increment}>Increment the count!</button>
            <button onClick={decrement}>Decrement the count!</button>
        </div>
    );
}

export default FunctionalCounter;