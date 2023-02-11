'use strict';

class LikeButtonClass extends React.Component {
  constructor(props) {
    super(props);
    this.state = { liked: false };
  }

  render() {
    if (this.state.liked) {
      return 'You liked this in class';
    }

    return React.createElement(
      'button',
      { onClick: () => this.setState({ liked: true }) },
      'Like class '+ this.props.test
    );
  }
}



function LikeButtonFunc(props) {
    const [liked, setLiked] = React.useState(false);
  
    if (liked) {
      return 'You liked this in func!';
    }
  
    return React.createElement(
      'button',
      {
        onClick: () => setLiked(true),
      },
      'Like in func ' + props.test
    );
  }



const domContainer = document.querySelector('#like_button_container_forClass');
const root = ReactDOM.createRoot(domContainer);
root.render(React.createElement(LikeButtonClass , {test:"#message#"}));



const domContainerFunc = document.querySelector('#like_button_container_forFunc');
const rootFunc = ReactDOM.createRoot(domContainerFunc);
rootFunc.render(React.createElement(LikeButtonFunc,{test:'!message!'}));


