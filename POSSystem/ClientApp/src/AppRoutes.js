import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import BillingInformation from "./components/BillIngformation/BillingInformation";

const AppRoutes = [
  {
    index: true,
        element: <BillingInformation />
  },
  {
    path: '/counter',
    element: <Counter />
  },
  {
    path: '/fetch-data',
    element: <FetchData />
  }
];

export default AppRoutes;
