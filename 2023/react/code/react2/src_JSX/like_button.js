'use strict';

function LikeButton(props) {
  const [nr, setNr] = React.useState(1);
  let x=1;
  return (
    <button onClick={() => {
      setNr(nr=>nr+1);
      x++;
      if(props.msgToParent)
      props.msgToParent(nr);

    }}>
      Like {nr} {x} {props.test}
    </button>
  );
}
function DivTest() {
return (<div>Test</div>);
}

function LikeButtonNew() {

  const messageFromChild=(nr)=>{
    console.log("I have received from child" + nr);
  }
  
  return (
    <span>
    <LikeButton test="Andrei" msgToParent={messageFromChild} ></LikeButton>
    <DivTest></DivTest>
    <LikeButton test="Ignat"></LikeButton>
    </span>
  )
}

const rootNode = document.getElementById('like_button_root');
const root = ReactDOM.createRoot(rootNode);
root.render(<LikeButtonNew />);