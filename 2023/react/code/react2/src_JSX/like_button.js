'use strict';

function LikeButton() {
  const [liked, setLiked] = React.useState(false);

  if (liked) {
    return 'You liked this!';
  }

  return (
    <button onClick={() => setLiked(true)}>
      Like
    </button>
  );
}
function DivTest() {
return (<div>Test</div>_;
}

function LikeButtonNew() {

  return (
    <span>
    <LikeButton></LikeButton>
<DivTest></DivTest>
    <LikeButton></LikeButton>
    </span>
  )
}

const rootNode = document.getElementById('like_button_root');
const root = ReactDOM.createRoot(rootNode);
root.render(<LikeButtonNew />);