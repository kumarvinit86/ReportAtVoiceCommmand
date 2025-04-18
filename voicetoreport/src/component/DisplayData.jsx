import { useSelector } from 'react-redux'; // Importing useSelector to access Redux state
import './displayData.css'; // Importing CSS for styling

const DisplayData = () => {
  // Accessing data and error from the Redux store
  const data = useSelector((state) => state.api.data);
  const error = useSelector((state) => state.api.error);

  return (
    <div className="data-container">
      {/* Display error message if there is an error */}
      {error ? (
        <p className="data-error">Error: {error}</p>
      ) : data && data.length > 0 ? (
        // Display data in a table if data exists and is not empty
        <table className="data-table">
          <thead>
            <tr>
              {/* Dynamically generate table headers based on keys of the first data object */}
              {Object.keys(data[0]).map((key) => (
                <th key={key}>{key}</th>
              ))}
            </tr>
          </thead>
          <tbody>
            {/* Dynamically generate table rows and cells based on data */}
            {data.map((item, index) => (
              <tr key={index}>
                {Object.values(item).map((value, idx) => (
                  <td key={idx}>{value}</td>
                ))}
              </tr>
            ))}
          </tbody>
        </table>
      ) : (
        // Display message if no data is available
        <p>No data available</p>
      )}
    </div>
  );
};

export default DisplayData; // Exporting the component for use in other parts of the application