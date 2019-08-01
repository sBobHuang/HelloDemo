
import React, { Component } from 'react';

export class MessageBoard extends Component {


    static displayName = MessageBoard.name;

    constructor(props) {
        super(props);
        this.state = { messageboards: [], loading: true };
    }

    componentDidMount() {
        this.populateWeatherData();
    }

    static renderForecastsTable(messageboards) {
        return (
            <table className='table table-striped'>
                <thead>
                    <tr>
                        <th>title</th>
                        <th>name</th>
                        <th>message</th>
                        <th>leaveDate</th>
                    </tr>
                </thead>
                <tbody>
                    {messageboards.map(messageboard =>
                        <tr key={messageboard.name}>
                            <td>{messageboard.title}</td>
                            <td>{messageboard.name}</td>
                            <td>{messageboard.message}</td>
                            <td>{messageboard.leaveDate}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : MessageBoard.renderForecastsTable(this.state.messageboards);

        return (
            <div>
                <h1>留言板呀</h1>
                <p>这就是服务器所有的留言</p>
                {contents}
            </div>
        );
    }

    async populateWeatherData() {
        const response = await fetch('Messageboard');
        const data = await response.json();
        this.setState({ messageboards: data, loading: false });
    }
}