import React, { Component } from 'react';

interface CounterClassState {
    count: number;
}

class ClassBasedCounter extends Component<{}, CounterClassState> {

    constructor(props: {}) {
        super(props);

        this.state = { count: 0 };
    }

    increment = () => this.setState({ count: this.state.count + 1 })

    decrement = () => this.setState({ count: this.state.count - 1 })

    render() {
        return (
            <div>
                <h3>This is my class based counting-component</h3>
                <br />
                <p>Count: {this.state.count}</p>
                <button onClick={this.increment}>Increment the class based count!</button>
                <button onClick={this.decrement}>Decrement the class based count!</button>
            </div>
        );
    }

}

export default ClassBasedCounter;