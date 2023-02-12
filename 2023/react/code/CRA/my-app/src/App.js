import logo from './logo.svg';
import './App.css';
import GrandFather from './GrandFather'; 
import Father from './Father';
import Brother from './Brother';
import Sister from './Sister';
function App() {
  return (
    <div className="App">
      <header className="App-header">
        <img width={100} height={100} src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.js</code> and save to reload.
        </p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        > 
          Learn React
        </a>
      </header>

      <GrandFather ></GrandFather>

      <GrandFather >
      <Father></Father>

      </GrandFather>
        
          <Brother></Brother>
          <Sister></Sister>
    </div>
  );
}

export default App;
