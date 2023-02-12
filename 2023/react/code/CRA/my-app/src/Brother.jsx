import React , {useState,useEffect} from 'react';
function Brother(){

    const [count, setCount] = useState(0);
    const [anotherCount, setanotherCount] = useState(0);
    const increaseCount = ()=>{
        setCount(c=>c+ 1);
    }
    const increaseAnotherCount = ()=>{
        setanotherCount(c=>c+ 1);
    }

    useEffect(() => {
        console.log('test count')
        document.title = `You clicked count  ${count}  times`;

      },[count]);

      useEffect(() => {
        console.log('test another count')
        document.title = `You clicked  another ${anotherCount} times`;

      });
    
    return (
        <>
        <div>I am brother</div>
        <button onClick={increaseCount}>
            Click {count} and see 2 messages in console
        </button>

        <button onClick={increaseAnotherCount}>
            Click {anotherCount}  and see 1 message in console
        </button>
      </>
    )

}


export default Brother;