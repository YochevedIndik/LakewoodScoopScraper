import React, {useState, useEffect} from 'react';
import axios from 'axios';

const Home = () => {
    const [articles, setArticles] = useState([]);
   
    
    
 useEffect(() => {
        const getArticles=async()=>{
            const { data } = await axios.get(`/api/lakewoodscoop/scrape`);
             setArticles(data);
            }
            getArticles();
         
          }, []); 
        
        
    
        return (
            <>
            <h1>Welcome to the Lakewood Scoop Ad Free!!</h1>
           <div className='container mt-5'>
                <div className='row'>
                
                  
                </div>
                <div className='row mt-3'>
                    <div className='col-md-12'>
                    {!!articles.length && <table className='table table-hover table-striped table-bordered'>
                        <thead>
                            <tr>
                                <th>Image</th>
                                <th>Title</th>
                                <th>Text</th>
                                <th>Comments</th>
                            </tr>
                        </thead>
                        <tbody>
                            {articles.map((article, idx) => {
                                return <tr key={idx}>
                                    <td><img src={article.imageUrl} /></td>
                                    <td>
                                        <a href={article.link} target='_blank'>{article.title}</a>
                                    </td>
                                    <td>
                                        {article.text}
                                    </td>
                                    <td>{article.amountOfComments}</td>
                                </tr>
                            })}
                        </tbody>
                        </table>}
                        </div>
                </div>
            </div>
            </>
        )
                        }
    
    
    export default Home;