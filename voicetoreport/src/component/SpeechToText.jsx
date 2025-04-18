import React, { useState } from 'react';
import { useDispatch } from 'react-redux';
import { setData, setError } from '../datastore/apiSlice';
import * as SpeechSDK from 'microsoft-cognitiveservices-speech-sdk';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faMicrophone } from '@fortawesome/free-solid-svg-icons';
import './speechToText.css';

const SpeechToText = () => {
  // State to store the transcribed text
  const [transcript, setTranscript] = useState('');
  
  // Redux dispatch function to update the global state
  const dispatch = useDispatch();

  // Function to start speech recognition
  const startRecognition = () => {
    // Clear the transcript and reset the data in the Redux store
    setTranscript('');
    dispatch(setData([]));

    // Configure the Speech SDK with subscription key and region
    const speechConfig = SpeechSDK.SpeechConfig.fromSubscription(
      '<YOUR_SUBSCRIPTION_KEY>',
      '<eastus>' // Replace with your region
    );
    speechConfig.speechRecognitionLanguage = 'en-US';

    // Configure audio input from the default microphone
    const audioConfig = SpeechSDK.AudioConfig.fromDefaultMicrophoneInput();

    // Create a speech recognizer instance
    const recognizer = new SpeechSDK.SpeechRecognizer(speechConfig, audioConfig);

    // Perform speech recognition once and handle the result
    recognizer.recognizeOnceAsync(result => {
      setTranscript(result.text); // Update the transcript with recognized text

      // Delay for 1 second before calling the API
      setTimeout(() => {
        callApi(result.text);
      }, 1000);
    });
  };

  // Function to call the API with the transcribed text
  const callApi = async (text) => {
    try {
      // Make a GET request to the API with the transcribed text as a query parameter
      const response = await fetch(
        `<apiurlhere>/reportextractor?commandText=${encodeURIComponent(text)}`,
        {
          method: 'GET',
          headers: {
            'accept': 'application/json',
          },
        }
      );

      // Check if the response is successful
      if (!response.ok) {
        throw new Error('API call failed');
      }

      const data = await response.json();

      // Handle the API response
      if (data.requestStatus === 200) {
        setTranscript(data.ResponseText); // Update the transcript with the API response
        dispatch(setData(data.result)); // Update the Redux store with the API result
      } else {
        throw new Error(data.RequestStatus);
      }

      console.log('API Response:', data);
    } catch (error) {
      console.error('Error calling API:', error);
      dispatch(setError(error.message)); // Dispatch an error to the Redux store
    }
  };

  return (
    <div className="speech-container">
      {/* Button to start speech recognition */}
      <div style={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between', width: '20%' }}>
        <button className="speech-button" onClick={startRecognition}>
          <FontAwesomeIcon icon={faMicrophone} />
        </button>
      </div>

      {/* Display the transcribed text */}
      <div>
        <span className="speech-transcription">Transcription: {transcript}</span>
      </div>
    </div>
  );
};

export default SpeechToText;
