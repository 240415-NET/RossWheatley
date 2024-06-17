import React from 'react';
import logo from './logo.svg';
import './App.css';
import FunctionalCounter from './components/FunctionalCounter';
import ClassBasedCounter from './components/ClassBasedCounter';

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <h1>React Counter Functional/Class Demo</h1>
        <FunctionalCounter />
        <br />
        <ClassBasedCounter />
      </header>
    </div>
  );
}

export default App;
