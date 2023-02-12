import logo from './logo.svg';
import './App.css';
import GrandFather from './GrandFather'; 
import Brother from './Brother';
function App() {
const arrNumbers=Array.from(
  {length: 3},
  (item, index) => item = index + 1
);

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

      <h1>See document title ?</h1>
      <h1>A single GrandFather</h1>
      <GrandFather></GrandFather>
      <h1>And a array of GrandFathers</h1>

      {arrNumbers.map(function(_, index){
                return <>
                <h1>Grandfather {index+1}</h1>
                <GrandFather></GrandFather>
                </>
              })}

    </div>
  );
}

export default App;
