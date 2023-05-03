/* // eslint-disable*/

import React, {useState} from 'react';
import logo from './logo.svg';
import './App.css';

function App() {

  let [postTitle, change] = useState(['post1', 'post2', 'post3']);
  let [good, change2] = useState(0);
  let [bad, change3] = useState(0);
  
  let title = 'TEST REACT';

function postTitleChange() {
  var newArray = [...postTitle];
  newArray[0] = '여자코트 추천';
  change(newArray);
}


  return (
    <div className="App">
      <div className="black-nav">
        <div> {title}</div>  
      </div>  
      <button onClick={()=>{postTitleChange()}}>
        버튼
      </button>
      {/* <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.js</code> and save to reload.
        </p>
         
      </header> */}
      <div className="list">
        <h3> {postTitle[0]}
          <span onClick={()=>{change2(good+=1)}}>😁</span> 
          {good}
        </h3>
        <p>😁😁😁</p>
      </div>
      <div className="list">
        <h3> {postTitle[1]}
          <span onClick={()=>{change3(bad+=1)}}>😒</span> 
          {bad}
        </h3>
        <p>😒😒😒</p>
      </div>
      <div className="list">
        <h3> {postTitle[2]}</h3>
        <p>file:///C:/Users/otata/Downloads/MicrosoftWindows.Client.CBS_cw5n1h2txyewy!InputApp/MuaKissGIF.gif</p>
      </div>

      
      <Modal></Modal>
    </div>
  );
}


function Modal(){
  return (
    <>
      <div className="modal">
        <h2> 제목</h2>
        <p>날짜</p>
        <p>상세 내용</p>
      </div>
    </>
  )
}
export default App;
