import React, { Fragment, useEffect, useState } from 'react';
import axios from 'axios';

export default function BillingInformation() {

    const date1 = new Date().toISOString().split('T')[0];
    const initialTime = formatTime(new Date());

    const [date, setDate] = useState(date1);
    const [time, setTime] = useState(initialTime);



    const [items, setItems] = useState([]);
    const [item, setItem] = useState(0);

    console.log("items", items)

    console.log("item", item)

    const [quantity, setQuantity] = useState(0);
    const [price, setPrice] = useState(0);
    const [amount, setAmount] = useState(0);


    const [subTotal, setSubTotal] = useState(0);
    const [discount, setDiscount] = useState(0);
    const [vat, setVat] = useState(0);
    const [grandTotal, setGrandTotal] = useState(0);
    const [dataModel, setDataModel] = useState([]);

    useEffect(() => {
        var items = getAllItems();
        setDate(date1);
        setVat(12);
    }, []);

    useEffect(() => {
        calculateSubTotal();

    }, [dataModel]);

    useEffect(() => {
        handleAmount();

    }, [quantity]);

    useEffect(() => {
        handleGrandTotal();

    }, [dataModel,discount ]);

    useEffect(() => {
        const selectedItem = items.find(x => x.itemId === parseInt(item));
        console.log("selectedItem", selectedItem)
        const selectedPrice = selectedItem ? selectedItem.price : 0;

        setPrice(selectedPrice);
    }, [item]);

    function formatTime(date) {
        const hours = date.getHours().toString().padStart(2, '0');
        const minutes = date.getMinutes().toString().padStart(2, '0');
        return `${hours}:${minutes}`;
    }

    async function getAllItems() {
        var result = await axios.get(`http://localhost:44416/api/Item/GetAllItems`);
        setItems(result.data);

    }

    const handleClick = () => {
        const newItem = {
            itemId: parseInt(item),
            quantity: parseFloat( quantity),
            price: price,
            amount: parseFloat(quantity) * price,
        };

        setDataModel((prevData) => [...prevData, newItem]);

    }

    function calculateSubTotal() {
        if (dataModel.length === 0) {
            setSubTotal(0);
            return;
        }

        var total = dataModel.reduce((acc, item) => acc + parseFloat(item.quantity) * parseFloat(item.price), 0);
        console.log("total", total)
        setSubTotal(total);
    }

    function handleAmount() {

        setAmount(parseFloat(quantity) * price);
    }

    function handleGrandTotal() {

        var total = parseFloat(subTotal + parseFloat((subTotal * vat) / 100) - parseFloat((subTotal * discount) / 100)).toFixed(2);
        setGrandTotal(total);
    }

    function handleReset() {
        setItem(0);
        setQuantity(0);
        setPrice(0);
        setAmount(0);
    }


     function PaidBillAmounts() {
        
        var paidmodel = {
            billInginformationId: 0,
            subTotal: subTotal,
            discount: parseFloat(discount),
            vat: parseFloat(vat),
            grandTotal: parseFloat(grandTotal),
            items: dataModel,
        };

        console.log("paidmodel", paidmodel)
        var result = postData(paidmodel)

        if (result.data == true) {
            alert("success");
            cleardata();
        }
        else {
            alert("failed");
        }

    }

    async function postData(paidmodel) {
        return await axios.post(`http://localhost:44416/api/BillingInformation/SaveBillingInformation`, paidmodel);
    }

    function cleardata() {

        setItems([]);
        setQuantity(0);
        setAmount(0);
        setDiscount(0);
        setPrice(0);
        setVat(0);
        setGrandTotal(0);
        setSubTotal(0);
    }


    return (
        <Fragment>
            <h1>Billing Info</h1>

            <div>
                <label>Date</label>&nbsp;
                <input type="date" name="date" value={date} onChange={(e) => setDate(e.target.value)} /><br />

                <label>Time</label>&nbsp;
                <input type="time" name="time" value={time} onChange={(e) => setTime(e.target.value)} />


            </div>

            <div>
                <label>Items</label>
                <select name="items" value={item ? item.itemId : ""} onChange={(e) => setItem(e.target.value)}>
                    <option value="">Select an item</option>

                    {Array.isArray(items) && items.map((item) => (
                        <option key={item.itemId} value={item.itemId}>
                            {item.itemName} - {item.price}
                        </option>
                    ))}
                </select>

                <label>Quantity</label>
                <input type="number" name={quantity} value={quantity} onChange={(e) => setQuantity(e.target.value)} />

                <label>Price/Units</label>
                <input type="text" name={price} value={price} readOnly />

                <label>Amount</label>
                <input type="text" name={amount} value={amount} readOnly /><br />

                <button onClick={() => handleClick()}>Add to bill</button>
                <button onClick={() => handleReset()}>Reset</button>

            </div>

            <div>
                <table>
                    <thead>
                        <tr>
                            <th>Item</th>&nbsp;
                            <th>Quantity</th>&nbsp;
                            <th>Price/Units</th>&nbsp;
                            <th>Amount</th>
                        </tr>
                    </thead>

                    <tbody>
                        {
                            dataModel.length > 0 ?
                                dataModel.map((item, index) => {
                                    console.log("item", item)
                                    return (
                                        <tr>
                                            <th>{item.itemName}</th>&nbsp;
                                            <th>{item.quantity}</th>&nbsp;
                                            <th>{item.price}</th>&nbsp;
                                            <th>{item.amount}</th>

                                        </tr>

                                    )

                                }) : null
                        }
                    </tbody>

                </table>

            </div>
            <label>Sub Total</label>
            <input type="text" name={subTotal} value={subTotal} onChange={(e) => setSubTotal(e.target.value)} readOnly />

            <label>Discount</label>
            <input type="text" name={discount} value={discount} onChange={(e) => setDiscount(e.target.value)} />

            <label>VAT</label>
            <input type="text" name={vat} value={vat} readOnly />

            <label>Grand Total</label>
            <input type="text" name={grandTotal} value={grandTotal} readOnly />
            <div>

            </div>

            <div>
                <button onClick={() => PaidBillAmounts()}>Amount paid & Add New Bill</button>

            </div>
        </Fragment>

    )
}